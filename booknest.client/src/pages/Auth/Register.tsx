import { useState } from "react";

import { register } from '../../api/user';

import { useAuth } from '../../AuthContext';
import { useNavigate } from "react-router-dom";

const Register: React.FC = () => {
    const { login } = useAuth();

    const navigate  = useNavigate();

    const [email, setEmail] = useState("");

    const [password, setPassword] = useState("");

    const [fullname, setFullname] = useState("");

    const [phone, setPhone] = useState("");

    const [error, setError] = useState("");

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            const loginResponse = await register({ email, fullname, password, phone });
            login(loginResponse.jwtToken);
            navigate('/');
        }
        catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            }
        }
    };

    return (
        <div className='form-container'>
            <form onSubmit={handleSubmit} className='my-auto vertical gap-3'>
                <input
                    type='email'
                    placeholder='email'
                    value={email}
                    onChange={(e) => setEmail(e.target.value)} />
                <input
                    type='password'
                    placeholder='password'
                    value={password}
                    onChange={(e) => setPassword(e.target.value)} />
                <input
                    type='text'
                    placeholder='fullname'
                    value={fullname}
                    onChange={(e) => setFullname(e.target.value)} />
                <input
                    type='text'
                    placeholder='phoneNumber'
                    value={phone}
                    onChange={(e) => setPhone(e.target.value)} />
                <button type='submit' className='btn btn-primary'>Register</button>
                <p>{error}</p>
            </form>
        </div>
    );
}

export default Register;