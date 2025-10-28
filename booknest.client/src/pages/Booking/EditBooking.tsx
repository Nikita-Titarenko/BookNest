import { useEffect, useState, type FormEvent } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { getRoomBooking, updateRoomBooking } from "../../api/user-room";

const EditBooking: React.FC = () => {
    const [startDate, setStartDate] = useState("");

    const [endDate, setEndDate] = useState("");

    const [error, setError] = useState("");

    const navigate = useNavigate();

    const [searchParams] = useSearchParams();

    const roomId = searchParams.get('room-id');

    const parseRoomId = roomId != null ? Number(roomId) : null;

    useEffect(() => {
        

        const getBookingFetch = async () => {
            if (parseRoomId) {
                const roomBooking = await getRoomBooking(parseRoomId);
                setStartDate(roomBooking.startDate);
                setEndDate(roomBooking.endDate);
            }
        };

        getBookingFetch();
    }, []);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            if (parseRoomId) {
                await updateRoomBooking({ startDate, endDate, roomId: parseRoomId });
                navigate('/my-bookings');
            }
        }
        catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            }
        }
    }

    return (
        <div className='form-container'>
            <form onSubmit={handleSubmit} className='my-auto vertical gap-3'>
                <input
                    type='date'
                    value={startDate}
                    onChange={(e) => { setStartDate(e.target.value) }}
                ></input>
                <input
                    type='date'
                    value={endDate}
                    onChange={(e) => { setEndDate(e.target.value) }}
                ></input>
                <button type='submit' className='btn btn-primary'>Update room booking</button>
                <p>{error}</p>
            </form>
        </div>
    );
}

export default EditBooking;