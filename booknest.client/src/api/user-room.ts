const API_BASE = 'https://localhost:7079/api/';

export interface BookRoomData {
    roomId: number,
    startDate: string,
    endDate: string
}

export interface RoomBookingData {
    startDate: string,
    endDate: string,
}

export interface RoomBookingByUserData {
    roomId: number,
    hotelId: number,
    hotelName: string,
    roomName: string,
    startDate: string,
    endDate: string,
}

export interface RoomBookingByHotelData {
    startDate: string,
    endDate: string,
    roomName: string,
    appUserEmail: string,
    appUserFullname: string,
    phoneNumber: string,
    bookingsTotalCount: number
}

export interface AuditRoomBookingByHotelData {
    auditAppUserRoomId: number,
    oldStartDate: string,
    newStartDate: string,
    oldEndDatedate: string,
    newEndDate: string,
    roomName: string,
    appUserEmail: string,
    appUserFullname: string,
    phoneNumber: string,
    actionType: string,
    actionDateTime: string
}

export const getRoomBooking = async (roomId: number): Promise<RoomBookingData> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to get room booking');
    }

    const response = await fetch(`${API_BASE}UserRooms/${roomId}`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        },
    });

    const data = await response.json();

    if (!response.ok) {
        throw new Error(data[0].metadata.Code);
    }

    return data;
};

export const getRoomBookingsByUser = async (): Promise<RoomBookingByUserData[]> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to get your room bookings');
    }

    const response = await fetch(`${API_BASE}UserRooms/by-user`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        },
    });

    const data = await response.json();

    if (!response.ok) {
        throw new Error(data[0].metadata.Code);
    }

    return data;
};

export const getRoomBookingsByHotel = async (hotelId: number): Promise<RoomBookingByHotelData[]> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to get your room bookings');
    }

    const response = await fetch(`${API_BASE}UserRooms/by-hotel?hotelId=${hotelId}`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        },
    });

    const data = await response.json();

    if (!response.ok) {
        throw new Error(data[0].metadata.Code);
    }

    return data;
};

export const getAuditRoomBookingsByHotel = async (hotelId: number): Promise<AuditRoomBookingByHotelData[]> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to get your rooms');
    }

    const response = await fetch(`${API_BASE}UserRooms/audit-by-hotel?hotelId=${hotelId}`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        },
    });

    const data = await response.json();

    if (!response.ok) {
        throw new Error(data[0].metadata.Code);
    }

    return data;
};

export const bookRoom = async (data: BookRoomData): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to book room');
    }
    const response = await fetch(`${API_BASE}UserRooms`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        const json = await response.json();
        if (json[0].metadata.Code === 50015) {
            throw new Error('All rooms already booked on this date');
        }
        if (json[0].metadata.Code === 50016) {
            throw new Error('Booking already exists');
        }
        throw new Error(json[0].message);
    }
};

export const updateRoomBooking = async (data: BookRoomData): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to update room booking');
    }
    const response = await fetch(`${API_BASE}UserRooms/${data.roomId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        const json = await response.json();
        if (json[0].metadata.Code === 50015) {
            throw new Error('All rooms already booked on this date');
        }
        throw new Error(json[0].message);
    }
};

export const deleteRoomBooking = async (roomId: number): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to delete room booking');
    }
    const response = await fetch(`${API_BASE}UserRooms/${roomId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });

    if (!response.ok) {
        const json = await response.json();
        throw new Error(json[0].message);
    }
};