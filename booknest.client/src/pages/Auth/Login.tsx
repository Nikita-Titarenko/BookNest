import { useState } from 'react';

import { login as apiLogin } from '../../api/user';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../AuthContext';

const Login: React.FC = () => {

    const [email, setEmail] = useState('');

    const [password, setPassword] = useState('');

    const [error, setError] = useState('');

    const navigate = useNavigate();

    const { login } = useAuth();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            const loginResponse = await apiLogin(email, password);

            login(loginResponse.jwtToken);
            navigate('/');
        }
        catch (err){
            if (err instanceof Error) {
                setError(err.message);
            }
        }

        
    };
    return (
        <div className="form-container">
            <form onSubmit={handleSubmit} className="my-auto vertical gap-3 w-25">
                <input
                    type="email"
                    placeholder="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)} />
                <input
                    type="password"
                    placeholder="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)} />
                <button type="submit" className="btn btn-primary">Login</button>
                <p>{error}</p>
            </form>
        </div>
  );
}

export default Login;