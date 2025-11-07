import { useEffect, useState, type FormEvent } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { getHotel, updateHotel } from '../../api/hotel';
import { ValidationError } from '../../errors/validation-error';

interface FormErrors {
    hotelName?: string;
    hotelCity?: string;
    hotelDescription?: string;
}

const EditHotel: React.FC = () => {
    const [hotelName, setHotelName] = useState('');

    const [hotelCity, setHotelCity] = useState('');

    const [hotelDescription, setHotelDescription] = useState('');

    const [errors, setErrors] = useState<FormErrors>({});

    const [generalError, setGeneralError] = useState('');

    const navigate = useNavigate();

    const [searchParams] = useSearchParams();

    useEffect(() => {
        const id = searchParams.get('id');

        const getHotelFetch = async () => {
            if (id) {
                const hotel = await getHotel(Number(id));
                setHotelName(hotel.hotelName);
                setHotelDescription(hotel.hotelDescription);
                setHotelCity(hotel.hotelCity);
            }
        };

        getHotelFetch();
    }, []);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            const hotelId = searchParams.get('id');
            if (!hotelId) {
                setGeneralError('hotel id not specified');
                return;
            }
            await updateHotel(Number(hotelId), { hotelCity, hotelName, hotelDescription });
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
                    value={hotelName}
                    onChange={(e) => setHotelName(e.target.value)} />
                {errors.hotelName && <p className="error">{errors.hotelName}</p>}
                <input
                    type="text"
                    placeholder="City"
                    value={hotelCity}
                    onChange={(e) => setHotelCity(e.target.value)} />
                {errors.hotelCity && <p className="error">{errors.hotelCity}</p>}
                <textarea
                    placeholder="Description"
                    value={hotelDescription}
                    onChange={(e) => setHotelDescription(e.target.value)} />
                {errors.hotelDescription && <p className="error">{errors.hotelDescription}</p>}
                <button type="submit" className="btn btn-primary">Update hotel</button>
                <p>{generalError}</p>
            </form>
        </div>
    );
}

export default EditHotel;