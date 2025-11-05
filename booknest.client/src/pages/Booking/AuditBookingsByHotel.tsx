import { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import { getAuditRoomBookingsByHotel, type AuditRoomBookingByHotelData } from '../../api/user-room';

const AuditBookingsByHotel: React.FC = () => {
    const [bookings, setBookings] = useState<AuditRoomBookingByHotelData[]>([]);

    const [error, setError] = useState('');

    const [searchParams] = useSearchParams();

    useEffect(() => {
        const getBookings = async () => {
            const hotelId = searchParams.get('hotelId');
            if (hotelId) {
                try {
                    const hotels = await getAuditRoomBookingsByHotel(Number(hotelId));
                    setBookings(hotels);
                }
                catch (err) {
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
            <table className="mb-auto">
                <thead>
                    <tr>
                        <th>
                            <p>
                                Old start date
                            </p>
                        </th>
                        <th>
                            <p>
                                New start date
                            </p>
                        </th>
                        <th>
                            <p>
                                Old end date
                            </p>
                        </th>
                        <th>
                            <p>
                                New end date
                            </p>
                        </th>
                        <th>
                            <p>
                                Room name
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
                                Action
                            </p>
                        </th>
                        <th>
                            <p>
                                Action date
                            </p>
                        </th>
                        <th>
                            <p>
                                Action time
                            </p>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {
                        bookings.map((booking) => (
                            <tr key={`${booking.auditAppUserRoomId}` }>
                                <th>
                                    <p>
                                        {booking.oldStartDate}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.oldEndDatedate}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.newStartDate}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.newEndDate}
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
                                        {booking.actionType}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.actionDateTime.split('T')[0]}
                                    </p>
                                </th>
                                <th>
                                    <p>
                                        {booking.actionDateTime.split('T')[1].split('.')[0]}
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

export default AuditBookingsByHotel;