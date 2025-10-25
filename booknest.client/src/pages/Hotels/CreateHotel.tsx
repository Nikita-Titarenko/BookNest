import { useState, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { createHotel } from "../../api/hotel";

const CreateHotel: React.FC = () => {
    const [hotelName, setHotelName] = useState("");

    const [hotelCity, setHotelCity] = useState("");

    const [hotelDescription, setHotelDescription] = useState("");

    const [error, setError] = useState("");

    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            await createHotel({ hotelCity, hotelName, hotelDescription });
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
                <button type='submit'>Create hotel</button>
                <p>{error}</p>
            </form>
        </div>
    );
}

export default CreateHotel;