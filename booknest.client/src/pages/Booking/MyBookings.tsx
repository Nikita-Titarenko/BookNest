import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { deleteRoomBooking, getRoomBookingsByUser, type RoomBookingByUserData } from "../../api/user-room";

const MyBookings: React.FC = () => {
    const [myBookings, setMyBookings] = useState<RoomBookingByUserData[]>([]);

    const [error, setError] = useState("");

    useEffect(() => {
        const getRoomBookings = async () => {
            const roomBookings = await getRoomBookingsByUser();
            setMyBookings(roomBookings);
        };
        getRoomBookings();
    }, []);

    const deleteRoomBookingHandle = async (roomId: number) => {
        try {
            await deleteRoomBooking(roomId);
            const roomBookings = myBookings.filter((roomBooking) => {
                return roomBooking.roomId != roomId
            });

            setMyBookings(roomBookings);
        }
        catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            }
        }
    }

    return (
        <>
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
                                Room name
                            </p>
                        </th>
                        <th>
                            <p>
                                Start date
                            </p>
                        </th>
                        <th>
                            <p>
                                End date
                            </p>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        myBookings.map((roomBooking) => (
                            <tr key={roomBooking.hotelId.toString()}>
                                <th>
                                    <p>
                                        {roomBooking.hotelName}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {roomBooking.roomName}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {roomBooking.startDate}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {roomBooking.endDate}
                                    </p>
                                </th>
                                <th>
                                    <Link to={`/edit-booking?room-id=${roomBooking.roomId}`} className='btn btn-secondary'>
                                        Edit
                                    </Link>
                                </th>
                                <th>
                                    <button onClick={() => { deleteRoomBookingHandle(roomBooking.roomId) }} className='btn btn-danger'>
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

export default MyBookings