import type React from 'react';
import { useEffect, useState } from 'react';
import { getHotel } from '../../api/hotel';
import type { HotelData } from '../../api/hotel';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { getRoomsByHotel, type RoomListItem } from '../../api/room';
import { bookRoom } from '../../api/user-room';

const HotelWithRooms: React.FC = () => {
    const formatDate = (date: Date): string => {
        return date.toISOString().split('T')[0];
    };

    const [searchParams] = useSearchParams();

    const [rooms, setRooms] = useState<RoomListItem[]>([]);

    const hotelId: string | null = searchParams.get('hotel-id');

    const parseHotelId: number | null = hotelId !== null ? Number(hotelId) : null;

    const navigate = useNavigate();

    const [startDate, setStartDate] = useState(() => {
        const startDate = searchParams.get('startDate');
        if (startDate) {
            return startDate;
        }

        return formatDate(new Date())
    });

    const [endDate, setEndDate] = useState<string>(() => {
        const endDate = searchParams.get('endDate');
        if (endDate) {
            return endDate;
        }
        const date = new Date();
        date.setDate(date.getDate() + 1);
        return formatDate(date);
    });

    const [hotel, setHotel] = useState<HotelData>();

    const [guestsNumber, setGuestsNumber] = useState<number | null>(() => {
        const getParam = searchParams.get('guestsNumber');
        if (getParam) {
            return Number(getParam);
        }

        return null;
    });

    const bookRoomHandle = async (roomId: number) => {
        try {
            await bookRoom({roomId, startDate, endDate});

            navigate('/my-bookings');
        }
        catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            }
        }
    }

    useEffect(() => {
        if (!parseHotelId) {
            setError('hotel id is not specified');
            return;
        }
        const loadHotel = async () => {
            try {
                const hotel = await getHotel(parseHotelId);
                setHotel(hotel);
            }
            catch (err) {
                if (err instanceof Error) {
                    setError(err.message);
                }
            }
        };
        loadHotel();
    }, []);

    useEffect(() => {
        const loadRooms = async () => {
            try {
                if (!parseHotelId) {
                    setError('hotel id is not specified');
                    return;
                }
                const rooms = await getRoomsByHotel(parseHotelId, startDate, endDate, guestsNumber);

                setRooms(rooms);
            }
            catch (err) {
                if (err instanceof Error) {
                    setError(err.message);
                }
            }
        };
        loadRooms();
    }, [startDate, endDate, guestsNumber]);

    const [error, setError] = useState('');

    return (
        <div>
            <form>
                <input
                    type="date"
                    value={startDate}
                    onChange={(e) => { setStartDate(e.target.value) }}
                ></input>
                <input
                    type="date"
                    value={endDate}
                    onChange={(e) => { setEndDate(e.target.value) }}
                ></input>
                <input
                    type="number"
                    placeholder="Guests number"
                    value={guestsNumber === null ? '' : guestsNumber}
                    onChange={(e) => { setGuestsNumber(Number(e.target.value)) }}
                ></input>
            </form>
            <p>{error}</p>
            <div className="text-left vertical mb-3 card">
                <h2>{hotel?.hotelName}</h2>
                <p>{hotel?.hotelCity}</p>
                <p>{hotel?.hotelDescription}</p>
            </div>
            <table className="mb-auto">
                <thead>
                    <tr>
                        <th>
                            <p>
                                Room name
                            </p>
                        </th>
                        <th>
                            <p>
                                Room price (UAH)
                            </p>
                        </th>
                        <th>
                            <p>
                                Room size (m<sup>2</sup>)
                            </p>
                        </th>
                        <th>
                            <p>
                                Guests number
                            </p>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        rooms.map((r) => (
                            <tr key={ r.roomId }>
                                <th>
                                    <p>
                                        {r.roomName}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {r.roomPrice}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {r.roomSize}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {r.guestsNumber}
                                    </p>
                                </th>
                                <th>
                                    <button onClick={() => { bookRoomHandle(r.roomId) }} className="btn btn-primary">
                                        Book
                                    </button>
                                </th>
                            </tr>
                        ))
                    }
                </tbody>
            </table>
        </div>);
};

export default HotelWithRooms;