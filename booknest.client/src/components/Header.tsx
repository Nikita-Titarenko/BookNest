import { Link } from 'react-router-dom';

import {useAuth} from '../AuthContext';

function Header() {
    const { isAuthenticated } = useAuth();

    return (
        <nav className="horizontal w-100 justify-content-between mb-5">
            <Link to='/' className='logo'>BookNest</Link>
            {isAuthenticated ?
                <div className="horizontal gap-3">
                    <Link to="/my-bookings" className='btn btn-primary'>My bookings</Link>
                    <Link to="/my-hotels" className='btn btn-primary'>My hotels</Link>
                    <Link to="/logout" className='btn btn-danger'>Logout</Link>
                </div>    :
            <div className="horizontal gap-3">
                    <Link to="/login" className='btn btn-primary'>Login</Link>
                    <Link to="/register" className='btn btn-secondary'>Register</Link>
            </div>}

        </nav>
    );
}

export default Header;