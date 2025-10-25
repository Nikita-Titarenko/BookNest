import { Link } from 'react-router-dom';

import {useAuth} from '../AuthContext';

function Header() {
    const { isAuthenticated } = useAuth();

    return (
        <nav className="horizontal w-100 justify-content-between">
            <h3 className="">BookNest</h3>
            {isAuthenticated ?
                <div className="horizontal gap-3">
                    <Link to="/my-hotels">My hotels</Link>
                    <Link to="/logout">Logout</Link>
                </div>    :
            <div className="horizontal gap-3">
                <Link to="/login">Login</Link>
                <Link to="/register">Register</Link>
            </div>}

        </nav>
    );
}

export default Header;