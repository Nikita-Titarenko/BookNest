import type React from 'react';
import { useEffect, useRef, useState, useCallback } from 'react';
import { getHotelsWithCheapestRoom, getHotelsWithMostExpensiveRoom } from '../../api/hotel';
import type { HotelWithRoom } from '../../api/hotel';
import { Link } from 'react-router-dom';

const HotelsWithRooms: React.FC = () => {
    type FilterItem = {
        value: string,
        label: string
    };

    const formatDate = (date: Date): string => date.toISOString().split('T')[0];

    const [startDate, setStartDate] = useState(formatDate(new Date()));
    const [endDate, setEndDate] = useState<string>(() => {
        const date = new Date();
        date.setDate(date.getDate() + 1);
        return formatDate(date);
    });
    const [guestsNumber, setGuestsNumber] = useState<number | null>(null);
    const [pageNumber, setPageNumber] = useState(1);
    const [hotelsWithRoom, setHotelsWithRoom] = useState<HotelWithRoom[]>([]);
    const [error, setError] = useState('');
    const [hasMore, setHasMore] = useState(true);
    const observer = useRef<IntersectionObserver | null>(null);
    const isFirstRender = useRef(true);

    const pageSize = 6;

    const filterItems: FilterItem[] = [
        { label: 'Most cheapest rooms', value: 'cheapest' },
        { label: 'Most expensive rooms', value: 'mostExpensive' },
    ];
    const [selectedFilter, setSelectedFilter] = useState(filterItems[0].value);

    const getHotels = async () => {
        try {
            if (selectedFilter === 'cheapest') {
                return await getHotelsWithCheapestRoom(startDate, endDate, pageNumber, pageSize, guestsNumber);
            } else if (selectedFilter === 'mostExpensive') {
                return await getHotelsWithMostExpensiveRoom(startDate, endDate, pageNumber, pageSize, guestsNumber);
            }
        } catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            }
        }

        return [];
    };

    const fetchHotels = async () => {
        const newHotels = await getHotels();
        setHotelsWithRoom(prev => [...prev, ...newHotels]);
        if (newHotels.length < pageSize) {
            setHasMore(false);
        }
    };

    useEffect(() => {
        fetchHotels();
    }, [pageNumber]);

    const lastHotelRef = useCallback((node: HTMLDivElement | null) => {
        if (!hasMore) {
            return;
        }
        if (observer.current) {
            observer.current.disconnect();
        } 
        observer.current = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting) {
                setPageNumber(prev => prev + 1);
            }
        });
        if (node) {
            observer.current.observe(node);
        }
    }, [hasMore]);

    useEffect(() => {
        setPageNumber(1);
        setHasMore(true);
        setHotelsWithRoom([]);
        if (isFirstRender.current) {
            isFirstRender.current = false;
        } else {
            fetchHotels();
        }
    }, [selectedFilter, startDate, endDate, guestsNumber]);

    return (
        <div>
            <form className="mb-3">
                <input type="date" value={startDate} onChange={e => setStartDate(e.target.value)} />
                <input type="date" value={endDate} onChange={e => setEndDate(e.target.value)} />
                <input
                    type="number"
                    placeholder="Guests number"
                    value={guestsNumber == null ? '' : guestsNumber}
                    onChange={e => setGuestsNumber(Number(e.target.value))}
                />
                <select value={selectedFilter} onChange={e => setSelectedFilter(e.target.value)}>
                    {filterItems.map(item => (
                        <option key={item.value} value={item.value}>{item.label}</option>
                    ))}
                </select>
            </form>
            <p>{error}</p>
            <div className="gap-3 d-flex flex-wrap">
                {hotelsWithRoom.map((hr, index) => {
                        return (
                            <div ref={index === hotelsWithRoom.length - 1 ? lastHotelRef : null} key={hr.hotelId} className="text-left vertical mb-3 card p-2">
                                <div className="vertical">
                                    <p>{hr.hotelName}</p>
                                    <p>{hr.hotelCity}</p>
                                </div>
                                <div className="horizontal justify-content-between">
                                    <p>{hr.roomName}</p>
                                    <p>{hr.roomPrice.toString()} UAH</p>
                                </div>
                                <Link to={`/hotel-with-rooms?hotel-id=${hr.hotelId}&startDate=${startDate}&endDate=${endDate}${guestsNumber != null ? `&guestsNumber=${guestsNumber})` : ''}`} className="ms-auto btn btn-primary">See</Link>
                            </div>
                        );
                })}
            </div>
            {!hasMore && <p className="text-center mt-3">No more hotels</p>}
        </div>
    );
};

export default HotelsWithRooms;
