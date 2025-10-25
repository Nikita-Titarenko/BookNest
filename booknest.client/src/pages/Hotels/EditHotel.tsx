import { useState, type FormEvent } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { updateHotel } from "../../api/hotel";
import type { HotelData } from "../../api/hotel";

const EditHotel: React.FC = () => {
    const [hotelName, setHotelName] = useState("");

    const [hotelCity, setHotelCity] = useState("");

    const [hotelDescription, setHotelDescription] = useState("");

    const [error, setError] = useState("");

    const navigate = useNavigate();

    const [searchParams] = useSearchParams();

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            const hotelId = searchParams.get('id');
            if (!hotelId) {
                setError('hotel id not specified');
                return;
            }
            await updateHotel(Number(hotelId), { hotelCity, hotelName, hotelDescription });
            navigate('/my-hotels');
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
                    value={hotelName}
                    onChange={(e) => setHotelName(e.target.value)} />
                <input
                    type='text'
                    placeholder='City'
                    value={hotelCity}
                    onChange={(e) => setHotelCity(e.target.value)} />
                <textarea
                    placeholder='Description'
                    value={hotelDescription}
                    onChange={(e) => setHotelDescription(e.target.value)} />
                <button type='submit'>Update hotel</button>
                <p>{error}</p>
            </form>
        </div>
    );
}

export default EditHotel;