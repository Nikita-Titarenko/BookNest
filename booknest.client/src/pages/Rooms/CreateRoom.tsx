import { useState, type FormEvent } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { createRoom } from '../../api/room';
import { ValidationError } from '../../errors/validation-error';

interface FormErrors {
    roomName?: string;
    roomPrice?: string;
    roomQuantity?: string;
    guestsNumber?: string;
    roomSize?: string;
}

const CreateRoom: React.FC = () => {
    const [roomName, setRoomName] = useState('');

    const [roomPrice, setRoomPrice] = useState<number | null>();

    const [roomQuantity, setRoomQuantity] = useState<number | null>();

    const [guestsNumber, setGuestsNumber] = useState<number | null>();

    const [roomSize, setRoomSize] = useState<number | null>();

    const [errors, setErrors] = useState<FormErrors>({});

    const [generalError, setGeneralError] = useState('');

    const navigate = useNavigate();

    const [searchParams] = useSearchParams();

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            if (!roomPrice || !roomQuantity || !guestsNumber || !roomSize) {
                setGeneralError('All fields must be entered');
                return;
            }
            const hotelId = searchParams.get('id');
            const parseHotelId = Number(hotelId);
            if (!hotelId) {
                setGeneralError('Hotel id not specified');
                return;
            }
            await createRoom({ roomPrice, roomName, guestsNumber, roomQuantity, roomSize, hotelId: parseHotelId });
            navigate(`/rooms-by-hotel?id=${hotelId}`);
        }
        catch (err) {
            if (err instanceof ValidationError) {
                const fieldErrors: FormErrors = {};
                err.errors.forEach(e => {
                    if (e.field) {
                        fieldErrors[e.field as keyof FormErrors] = e.message;
                    } else {
                        setGeneralError(e.message);
                    }
                });
                setErrors(fieldErrors);
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
                {errors.roomName && <p className="error">{errors.roomName}</p>}
                <input
                    type="number"
                    placeholder="Price (UAH)"
                    value={roomPrice === null ? '' : roomPrice}
                    onChange={(e) => setRoomPrice(Number(e.target.value))} />
                {errors.roomPrice && <p className="error">{errors.roomPrice}</p>}
                <input
                    type="number"
                    placeholder="Room quantity"
                    value={roomQuantity === null ? '' : roomQuantity}
                    onChange={(e) => setRoomQuantity(Number(e.target.value))} />
                {errors.roomQuantity && <p className="error">{errors.roomQuantity}</p>}
                <input
                    type="number"
                    placeholder="Size (m&sup2;)"
                    step="any"
                    value={roomSize === null ? '' : roomSize}
                    onChange={(e) => setRoomSize(Number(e.target.value))} />
                {errors.roomSize && <p className="error">{errors.roomSize}</p>}
                <input
                    type="number"
                    placeholder="Guests number"
                    value={guestsNumber === null ? '' : guestsNumber}
                    onChange={(e) => setGuestsNumber(Number(e.target.value))} />
                {errors.guestsNumber && <p className="error">{errors.guestsNumber}</p>}
                <button type="submit" className="btn btn-primary">Create room</button>
                <p>{generalError}</p>
            </form>
        </div>
    );
}

export default CreateRoom;