import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import { getRoomBookingsByHotel, type RoomBookingByHotelData } from "../../api/user-room";

const BookingsByHotel: React.FC = () => {
    const [bookings, setBookings] = useState<RoomBookingByHotelData[]>([]);

    const [error, setError] = useState("");

    const [searchParams] = useSearchParams();

    const hotelId = searchParams.get('hotelId');

    useEffect(() => {
        const getBookings = async () => {
            if (hotelId) {
                try {
                    const hotels = await getRoomBookingsByHotel(Number(hotelId));
                    setBookings(hotels);
                }
                catch (err){
                    if (err instanceof Error) {
                        setError(err.message);
                    }
                }
            }
        };
        getBookings();
    }, []);

    return (
        <>
            <div className='horizontal ms-auto'>
                <Link to={`/audit-bookings-by-hotel?hotelId=${hotelId}` } className='btn btn-primary'>See booking history</Link>
            </div>
            <table className='mb-auto'>
                <thead>
                    <tr>
                        <th>
                            <p>
                                Start date
                            </p>
                        </th>
                        <th>
                            <p>
                                End date
                            </p>
                        </th>
                        <th>
                            <p>
                                Rooms name
                            </p>
                        </th>
                        <th>
                            <p>
                                Email
                            </p>
                        </th>
                        <th>
                            <p>
                                Full name
                            </p>
                        </th>
                        <th>
                            <p>
                                Phone
                            </p>
                        </th>
                        <th>
                            <p>
                                Total bookings in this hotel
                            </p>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        bookings.map((booking) => (
                            <tr>
                                <th>
                                    <p>
                                        {booking.startDate}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.endDate}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.roomName}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.appUserEmail}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.appUserFullname}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.phoneNumber}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.bookingsTotalCount}
                                    </p>
                                </th>
                            </tr>
                        ))
                    }
                </tbody>
            </table>
            <p>{error}</p>
        </>
    );
}

export default BookingsByHotel;