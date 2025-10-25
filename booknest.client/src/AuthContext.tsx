import { createContext, useContext, useEffect, useState } from "react";

interface AuthContextType {
    isAuthenticated: boolean,
    login: (token: string) => void,
    logout: () => void
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{children: React.ReactNode}> = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const token = localStorage.getItem('jwt-token');
        setIsAuthenticated(!!token);
    }, []);

    const login = (token: string) => {
        localStorage.setItem('jwt-token', token);
        setIsAuthenticated(true);
    };

    const logout = () => {
        localStorage.removeItem('jwt-token');
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, login, logout }}>
            { children }
        </AuthContext.Provider>);
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('contex is null');
    }

    return context;
}