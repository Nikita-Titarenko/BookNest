import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css';
import './styles/index.css';
import Header from './components/Header';
import Footer from './components/Footer';
import { AuthProvider } from './AuthContext';
import HotelsWithRooms from './pages/Hotels/HotelsWithRooms';
import Login from './pages/Auth/Login';
import Register from './pages/Auth/Register';
import Logout from './pages/Auth/Logout';
import CreateHotel from './pages/Hotels/CreateHotel';
import EditHotel from './pages/Hotels/EditHotel';
import MyHotels from './pages/Hotels/MyHotels';
import HotelWithRooms from './pages/Hotels/HotelWithRooms';
import RoomsByHotel from './pages/Rooms/RoomsByHotel';
import EditRoom from './pages/Rooms/EditRoom';
import CreateRoom from './pages/Rooms/CreateRoom';
import MyBookings from './pages/Booking/MyBookings';
import EditBooking from './pages/Booking/EditBooking';
import BookingsByHotel from './pages/Booking/BookingsByHotel';
import AuditBookingsByHotel from './pages/Booking/AuditBookingsByHotel';

function App() {

    return (
        <AuthProvider>
            <Router>
                <Header />
                <main className=''>
                    <Routes>
                        <Route path="/" element={<HotelsWithRooms />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/logout" element={<Logout />} />
                        <Route path="/create-hotel" element={<CreateHotel />} />
                        <Route path="/my-hotels" element={<MyHotels />} />
                        <Route path="/edit-hotel" element={<EditHotel />} />
                        <Route path="/hotel-with-rooms" element={<HotelWithRooms />} />
                        <Route path="/rooms-by-hotel" element={<RoomsByHotel />} />
                        <Route path="/edit-room" element={<EditRoom />} />
                        <Route path="/create-room" element={<CreateRoom />} />
                        <Route path="/my-bookings" element={<MyBookings />} />
                        <Route path="/edit-booking" element={<EditBooking />} />
                        <Route path="/bookings-by-hotel" element={<BookingsByHotel />} />
                        <Route path="/audit-bookings-by-hotel" element={<AuditBookingsByHotel />} />
                    </Routes>
                </main>
                <Footer />
            </Router>
        </AuthProvider>
    );
}

export default App;