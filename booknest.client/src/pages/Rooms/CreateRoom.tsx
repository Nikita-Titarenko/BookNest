import { useState, type FormEvent } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { createRoom } from "../../api/room";

const CreateRoom: React.FC = () => {
    const [roomName, setRoomName] = useState("");

    const [roomPrice, setRoomPrice] = useState<number | null>();

    const [roomQuantity, setRoomQuantity] = useState<number | null>();

    const [guestsNumber, setGuestsNumber] = useState<number | null>();

    const [roomSize, setRoomSize] = useState<number | null>();

    const [error, setError] = useState("");

    const navigate = useNavigate();

    const [searchParams] = useSearchParams();

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            if (roomPrice == null || roomQuantity == null || guestsNumber == null || roomSize == null) {
                setError('All fields must be entered');
                return;
            }
            const hotelId = searchParams.get('id');
            const parseHotelId = Number(hotelId);
            if (!hotelId) {
                setError('Hotel id not specified');
                return;
            }
            await createRoom({ roomPrice, roomName, guestsNumber, roomQuantity, roomSize, hotelId: parseHotelId });
            navigate(`/rooms-by-hotel?id=${hotelId}`);
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
                    type='text'
                    placeholder='Hotel name'
                    value={roomName}
                    onChange={(e) => setRoomName(e.target.value)} />
                <input
                    type='number'
                    placeholder='Price (UAH)'
                    value={roomPrice == null ? '' : roomPrice}
                    onChange={(e) => setRoomPrice(Number(e.target.value))} />
                <input
                    type='number'
                    placeholder='Room quantity'
                    value={roomQuantity == null ? '' : roomQuantity}
                    onChange={(e) => setRoomQuantity(Number(e.target.value))} />
                <input
                    type='number'
                    placeholder='Size (m&sup2;)'
                    step='any'
                    value={roomSize == null ? '' : roomSize}
                    onChange={(e) => setRoomSize(Number(e.target.value))} />
                <input
                    type='number'
                    placeholder='Guests number'
                    value={guestsNumber == null ? '' : guestsNumber}
                    onChange={(e) => setGuestsNumber(Number(e.target.value))} />
                <button type='submit' className='btn btn-primary'>Create room</button>
                <p>{error}</p>
            </form>
        </div>
    );
}

export default CreateRoom;