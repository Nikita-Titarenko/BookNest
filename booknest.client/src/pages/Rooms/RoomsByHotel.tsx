import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import type { HotelListItem } from "../../api/hotel";
import { deleteHotel, getHotelsByUser } from "../../api/hotel";
import { getRoomsByHotel, type RoomListItem } from "../../api/room";

const RoomsByHotel: React.FC = () => {
    const [myRooms, setMyRooms] = useState<RoomListItem[]>([]);

    const [error, setError] = useState("");

    const [ searchParams ] = useSearchParams();

    useEffect(() => {
        const getRooms = async () => {
            const hotelId = searchParams.get('id');
            if (hotelId) {
                const rooms = await getRoomsByHotel(hotelId, null, null);
                setMyRooms(rooms);
            }

        };
        getRooms();
    }, []);

    const deleteRoomHandle = async (hotelId: number) => {
        try {
            await deleteHotel(hotelId);
            const rooms = myRooms.filter((hotel) => {
                return hotel.hotel_id != hotelId
            });

            setMyRooms(rooms);
        }
        catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            }
        }
    }

    return (
        <>
            <div className='horizontal ms-auto'>
                <Link to='/create-hotel'>Add hotel</Link>
            </div>
            <table className='mb-auto'>
                <thead>
                    <tr>
                        <th>
                            <p>
                                Hotel name
                            </p>
                        </th>
                        <th>
                            <p>
                                Hotel city
                            </p>
                        </th>
                        <th>
                            <p>
                                Rooms count
                            </p>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        myRooms.map((hotel) => (
                            <tr id={hotel.hotel_id.toString()}>
                                <th>
                                    <p>
                                        {hotel.hotel_name}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {hotel.hotel_city}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {hotel.total_room_count}
                                    </p>
                                </th>
                                <th>
                                    <Link to={`/rooms-by-hotel?id=${hotel.hotel_id}`}>
                                        See rooms
                                    </Link>
                                </th>
                                <th>
                                    <Link to={`/edit-hotel?id=${hotel.hotel_id}`}>
                                        Edit
                                    </Link>
                                </th>
                                <th>
                                    <button onClick={() => { deleteRoomHandle(hotel.hotel_id) }}>
                                        Delete
                                    </button>
                                </th>
                            </tr>
                        ))
                    }
                </tbody>
            </table>
            <p>{error}</p>
        </>
    );
}

export default RoomsByHotel;