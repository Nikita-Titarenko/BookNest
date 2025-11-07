import { useState, type FormEvent } from 'react';
import { useNavigate } from 'react-router-dom';
import { createHotel } from '../../api/hotel';
import { ValidationError } from '../../errors/validation-error';

interface FormErrors {
    hotelName?: string;
    hotelCity?: string;
    hotelDescription?: string;
}

const CreateHotel: React.FC = () => {
    const [hotelName, setHotelName] = useState('');

    const [hotelCity, setHotelCity] = useState('');

    const [hotelDescription, setHotelDescription] = useState('');

    const [errors, setErrors] = useState<FormErrors>({});

    const [generalError, setGeneralError] = useState('');

    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            await createHotel({ hotelCity, hotelName, hotelDescription });
            navigate('/my-hotels');
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
                    required
                    value={hotelName}
                    onChange={(e) => setHotelName(e.target.value)} />
                {errors.hotelName && <p className="error">{errors.hotelName}</p>}
                <input
                    type="text"
                    placeholder="City"
                    required
                    value={hotelCity}
                    onChange={(e) => setHotelCity(e.target.value)} />
                {errors.hotelCity && <p className="error">{errors.hotelCity}</p>}
                <textarea
                    placeholder="Description"
                    value={hotelDescription}
                    onChange={(e) => setHotelDescription(e.target.value)} />
                {errors.hotelDescription && <p className="error">{errors.hotelDescription}</p>}
                <button type="submit" className="btn btn-primary">Create hotel</button>
                <p>{generalError}</p>
            </form>
        </div>
    );
}

export default CreateHotel;