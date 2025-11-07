import { useState } from 'react';

import { login as apiLogin } from '../../api/user';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../AuthContext';
import { ValidationError } from '../../errors/validation-error';

interface FormErrors {
    email?: string;
    password?: string;
}

const Login: React.FC = () => {

    const [email, setEmail] = useState('');

    const [password, setPassword] = useState('');

    const [errors, setErrors] = useState<FormErrors>({});

    const [generalError, setGeneralError] = useState('');

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
            <form onSubmit={handleSubmit} className="my-auto vertical gap-3 w-25">
                <input
                    type="email"
                    placeholder="email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)} />
                {errors.email && <p className="error">{errors.email}</p>}
                <input
                    type="password"
                    placeholder="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)} />
                {errors.password && <p className="error">{errors.password}</p>}
                <button type="submit" className="btn btn-primary">Login</button>
                <p>{generalError}</p>
            </form>
        </div>
  );
}

export default Login;