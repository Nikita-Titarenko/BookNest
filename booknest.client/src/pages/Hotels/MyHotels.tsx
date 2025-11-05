import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import type { HotelListItem } from '../../api/hotel';
import { deleteHotel, getHotelsByUser } from '../../api/hotel';

const MyHotels: React.FC = () => {
    const [myHotels, setMyHotels] = useState<HotelListItem[]>([]);

    const [error, setError] = useState('');

    useEffect(() => {
        const getHotels = async () => {
            const hotels = await getHotelsByUser();
            setMyHotels(hotels);
        };
        getHotels();
    }, []);

    const deleteHotelHandle = async (hotelId: number) => {
        try {
            await deleteHotel(hotelId);
            const hotels = myHotels.filter((hotel) => {
                return hotel.hotelId != hotelId
            });

            setMyHotels(hotels);
        }
        catch (err) {
            if (err instanceof Error) {
                setError(err.message);
            }
        }
    }

    return (
      <>
      <div className="horizontal ms-auto">
                <Link to="/create-hotel" className="btn btn-primary">Add hotel</Link>
      </div>
            <table className="mb-auto">
              <thead>
                  <tr>
                      <th>
                          <p>
                            Hotel name
                          </p>
                      </th>
                      <th>
                          <p>
                              Hotel city
                          </p>
                      </th>
                      <th>
                          <p>
                              Rooms count
                          </p>
                      </th>
                  </tr>
              </thead>
              <tbody>
                  {
                      myHotels.map((hotel) => (
                          <tr key={ hotel.hotelId.toString() }>
                              <th>
                                  <p>
                                      { hotel.hotelName }
                                  </p>
                              </th>
                              <th>
                                  <p>
                                      { hotel.hotelCity }
                                  </p>
                              </th>
                              <th>
                                  <p>
                                      {hotel.totalRoomCount}
                                  </p>
                              </th>
                              <th>
                                  <Link to={`/bookings-by-hotel?hotelId=${hotel.hotelId}`} className="btn btn-secondary">
                                      See bookings
                                  </Link>
                              </th>
                              <th>
                                  <Link to={`/rooms-by-hotel?id=${hotel.hotelId}`} className="btn btn-secondary">
                                      See rooms
                                  </Link>
                              </th>
                              <th>
                                  <Link to={`/edit-hotel?id=${hotel.hotelId}`} className="btn btn-secondary">
                                      Edit
                                  </Link>
                              </th>
                              <th>
                                  <button onClick={() => { deleteHotelHandle(hotel.hotelId) }} className="btn btn-danger">
                                      Delete
                                  </button>
                              </th>
                          </tr>
                      ))
                  }
              </tbody>
            </table>
            <p>{ error }</p>
        </>
  );
}

export default MyHotels;