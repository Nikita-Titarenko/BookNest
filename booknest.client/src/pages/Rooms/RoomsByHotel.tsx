import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import { deleteRoom, getRoomsByHotel, type RoomListItem } from "../../api/room";
import { getHotelName } from "../../api/hotel";

const RoomsByHotel: React.FC = () => {
    const [myRooms, setMyRooms] = useState<RoomListItem[]>([]);

    const [hotelName, setHotelName] = useState('');

    const [error, setError] = useState("");

    const [searchParams] = useSearchParams();

    const [hotelId, setHotelId] = useState<Number>();

    useEffect(() => {
        const id = searchParams.get('id');
        setHotelId(Number(id));
        const getRooms = async () => {
            if (id) {
                const rooms = await getRoomsByHotel(Number(id), null, null, null);
                setMyRooms(rooms);
            }
        };
        
        getRooms();

        const getName = async () => {
            if (id) {
                const hotelName = await getHotelName(Number(id));
                setHotelName(hotelName.hotelName);
            }
        };

        getName();
    }, []);

    const deleteRoomHandle = async (roomId: number) => {
        try {
            await deleteRoom(roomId);
            const rooms = myRooms.filter((room) => {
                return room.roomId != roomId
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
                <Link to={`/create-room?id=${hotelId}` } className='btn btn-primary'>Add room</Link>
            </div>
            <h2>Rooms of the { hotelName } hotel</h2>
            <table className='mb-auto'>
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
                                Room quantity
                            </p>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        myRooms.map((room) => (
                            <tr key={ room.roomId }>
                                <th>
                                    <p>
                                        {room.roomName}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {room.roomPrice}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {room.roomSize}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {room.roomQuantity}
                                    </p>
                                </th>
                                <th>
                                    <Link to={`/edit-room?room-id=${room.roomId}&hotel-id=${hotelId}`} className='btn btn-secondary'>
                                        Edit
                                    </Link>
                                </th>
                                <th>
                                    <button onClick={() => { deleteRoomHandle(room.roomId) }} className='btn btn-danger'>
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