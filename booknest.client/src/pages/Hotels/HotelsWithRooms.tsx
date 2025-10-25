import type React from 'react';
import { useEffect, useState } from 'react';
import { getHotelsWithCheapestRoom, getHotelsWithMostExpensiveRoom } from '../../api/hotel';
import type { HotelWithRoom } from '../../api/hotel';
import { Link } from 'react-router-dom';

const HotelsWithRooms: React.FC = () => {
    type FilterItem = {
        value: string,
        label: string
    };

    const formatDate = (date: Date): string => {
        return date.toISOString().split('T')[0];
    };

    const [startDateTime, setStartDateTime] = useState(formatDate(new Date()));

    const [endDateTime, setEndDateTime] = useState<string>(() => {
        const date = new Date();
        date.setDate(date.getDate() + 1);
        return formatDate(date);
    });

    const [guestsNumber, setGuestsNumber] = useState<number | null>(null);

    const [pageNumber, setPageNumber] = useState(1);

    const [hotelsWithRoom, setHotelsWithRoom] = useState<HotelWithRoom[]>([]);

    const filterItems: FilterItem[] = [
        { label: 'Most cheapest rooms', value: 'cheapest' },
        { label: 'Most expensive rooms', value: 'mostExpensive' },
    ];

    const [selectedFilter, setSelectedFilter] = useState(filterItems[0].value);

    const [error, setError] = useState("");

    const pageSize = 10;

    useEffect(() => {
        const loadHotelsWithRooms = async () => {
            try {
                let hotels: HotelWithRoom[] = [];
                if (selectedFilter == 'cheapest') {
                    hotels = await getHotelsWithCheapestRoom(startDateTime, endDateTime, pageNumber, pageSize, guestsNumber);
                } else if (selectedFilter == 'mostExpensive'){
                    hotels = await getHotelsWithMostExpensiveRoom(startDateTime, endDateTime, pageNumber, pageSize, guestsNumber);
                }
                
                setHotelsWithRoom(hotels);
            }
            catch (err) {
                if (err instanceof Error) {
                    if (err.message == 'EmailOrPasswordIncorrect') {
                        setError('Login failed');
                    }
                }
            }
        };
        loadHotelsWithRooms();
    }, [startDateTime, endDateTime, pageNumber, guestsNumber, selectedFilter]);

    return (
        <div>
            <form>
                <input
                    type='date'
                    value={startDateTime}
                    onChange={(e) => { setStartDateTime(e.target.value) }}
                ></input>
                <input
                    type='date'
                    value={endDateTime}
                    onChange={(e) => { setEndDateTime(e.target.value) }}
                ></input>
                <input
                    type='number'
                    value={guestsNumber == null ? '' : guestsNumber}
                    onChange={(e) => { setGuestsNumber(Number(e.target.value)) }}
                ></input>
                <select
                    value={selectedFilter}
                    onChange={(e) => { setSelectedFilter(e.target.value) } }>
                    {
                        filterItems.map((item) => (
                            <option
                                key={item.value}
                                value={item.value}>
                                    {item.label}
                            </option>
                            )
                        )
                    }
                </select>
            </form>
            <p>{error}</p>
            <div className='gap-3 d-flex'>
                {
                    hotelsWithRoom.map((hr) => (
                        <div key={hr.hotel_id} className='text-left vertical mb-3 card'>
                            <div className='vertical'>
                                <p className=''>{hr.hotel_name}</p>
                                <p>{hr.hotel_city}</p>
                            </div>
                            <div className='horizontal justify-content-between'>
                                <p>{hr.room_name}</p>
                                <p>{hr.room_price.toString() + ' UAH'}</p>
                            </div>
                            <Link to='hotel-with-rooms' className='ms-auto'>See</Link>
                        </div>
                    ))
                }
            </div>
        </div>);
};

export default HotelsWithRooms;