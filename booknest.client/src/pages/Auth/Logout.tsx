import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../AuthContext";

const Logout: React.FC = () => {
    const navigate = useNavigate();

    const { logout } = useAuth();

    useEffect(() => {
        logout();
        navigate('/');
    }, [navigate, logout]);

    return null;
};

export default Logout;