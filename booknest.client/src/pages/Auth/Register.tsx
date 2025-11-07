import { useState } from "react";
import { register } from '../../api/user';
import { useAuth } from '../../AuthContext';
import { useNavigate } from 'react-router-dom';
import { ValidationError } from "../../errors/validation-error";

interface FormErrors {
    email?: string;
    password?: string;
    fullname?: string;
    phone?: string;
}

const Register: React.FC = () => {
    const { login } = useAuth();

    const navigate  = useNavigate();

    const [email, setEmail] = useState('');

    const [password, setPassword] = useState('');

    const [fullname, setFullname] = useState('');

    const [phone, setPhone] = useState('');

    const [errors, setErrors] = useState<FormErrors>({});

    const [generalError, setGeneralError] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            const loginResponse = await register({ email, fullname, password, phone });
            login(loginResponse.jwtToken);
            navigate('/');
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
    };


    return (
        <div className="form-container">
            <form onSubmit={handleSubmit} className="my-auto vertical gap-3">
                <div>
                    <input
                        type="email"
                        placeholder="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)} />
                    {errors.email && <p className="error">{errors.email}</p>}
                </div>

                <div>
                    <input
                        type="password"
                        placeholder="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)} />
                    {errors.password && <p className="error">{errors.password}</p>}
                </div>

                <div>
                    <input
                        type="text"
                        placeholder="fullname"
                        value={fullname}
                        onChange={(e) => setFullname(e.target.value)} />
                    {errors.fullname && <p className="error">{errors.fullname}</p>}
                </div>

                <div>
                    <input
                        type="text"
                        placeholder="phoneNumber"
                        value={phone}
                        onChange={(e) => setPhone(e.target.value)} />
                    {errors.phone && <p className="error">{errors.phone}</p>}
                </div>

                <button type="submit" className="btn btn-primary">Register</button>
            </form>
            <p>{ generalError }</p>
        </div>
    );
}

export default Register;