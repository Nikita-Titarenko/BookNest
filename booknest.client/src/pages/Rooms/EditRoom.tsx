import { useEffect, useState, type FormEvent } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { updateRoom, getRoom } from '../../api/room';

const EditRoom: React.FC = () => {
    const [roomName, setRoomName] = useState('');

    const [roomPrice, setRoomPrice] = useState<number | null>();

    const [roomQuantity, setRoomQuantity] = useState<number | null>();

    const [guestsNumber, setGuestsNumber] = useState<number | null>();

    const [roomSize, setRoomSize] = useState<number | null>();

    const [error, setError] = useState('');

    const navigate = useNavigate();

    const [searchParams] = useSearchParams();

    useEffect(() => {
        const id = searchParams.get('room-id');

        const getRoomFetch = async () => {
            if (id) {
                const room = await getRoom(Number(id));
                setRoomName(room.roomName);
                setRoomPrice(room.roomPrice);
                setRoomQuantity(room.roomQuantity);
                setGuestsNumber(room.guestsNumber);
                setRoomSize(room.roomSize);
            }
        };

        getRoomFetch();
    }, []);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            if (!roomPrice || !roomQuantity || !guestsNumber || !roomSize) {
                setError('All fields must be entered');
                return;
            }
            const roomId = searchParams.get('room-id');
            const parseRoomId = Number(roomId);
            const hotelId = searchParams.get('hotel-id');
            if (!roomId) {
                setError('Room id not specified');
                return;
            }
            await updateRoom(parseRoomId, { roomPrice, roomName, guestsNumber, roomQuantity, roomSize });
            if (!hotelId) {
                navigate(`/`);
            }
            navigate(`/rooms-by-hotel?id=${hotelId}`);
        }
        catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            }
        }
    }

    return (
        <div className="form-container">
            <form onSubmit={handleSubmit} className="my-auto vertical gap-3">
                <input
                    type="text"
                    placeholder="Hotel name"
                    value={roomName}
                    onChange={(e) => setRoomName(e.target.value)} />
                <input
                    type="number"
                    placeholder="Price (UAH)"
                    value={roomPrice === null ? '' : roomPrice}
                    onChange={(e) => setRoomPrice(Number(e.target.value))} />
                <input
                    type="number"
                    placeholder="Room quantity"
                    value={roomQuantity === null ? '' : roomQuantity}
                    onChange={(e) => setRoomQuantity(Number(e.target.value))} />
                <input
                    type="number"
                    placeholder="Size (m&sup2;)"
                    step="any"
                    value={roomSize === null ? '' : roomSize}
                    onChange={(e) => setRoomSize(Number(e.target.value))} />
                <input
                    type="number"
                    placeholder="Guests number"
                    value={guestsNumber === null ? '' : guestsNumber}
                    onChange={(e) => setGuestsNumber(Number(e.target.value))} />
                <button type="submit" className="btn btn-primary">Edit room</button>
                <p>{error}</p>
            </form>
        </div>
    );
}

export default EditRoom;