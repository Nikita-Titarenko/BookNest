const API_BASE = "https://localhost:7079/api/";

export interface RoomListItem {
    room_id: number,
    room_name: string,
    room_price: number,
    room_size: number,
    room_quantity: number,
};

export interface CreateRoomResponse {
    roomId: number
}

export interface RoomData {
    roomName: string,
    roomDescription: string,
    roomPrice: number,
    roomQuantity: number,
    guestsNumber: number,
    roomSize: number
}

export const getRoomsByHotel = async (hotelId: string, startDateTime: string | null, endDateTime: string | null): Promise<RoomListItem[]> => {
    const token = localStorage.getItem('jwt-token');
    if (token == null) {
        throw new Error('You must register to get your rooms');
    }

    const params = new URLSearchParams({
        hotelId: hotelId.toString(),
        ...(startDateTime != null && { startDateTime: startDateTime }),
        ...(endDateTime != null && { endDateTime: endDateTime })
    });

    const response = await fetch(`${API_BASE}Rooms/by-hotel?${params.toString()}`, {
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

export const createRoom = async (data: HotelData): Promise<CreateRoomResponse> => {
    const token = localStorage.getItem('jwt-token');
    if (token == null) {
        throw new Error('You must register to create a hotel');
    }
    const response = await fetch(`${API_BASE}Hotels`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
    });
    const json = await response.json();

    if (!response.ok) {
        if (json[0].metadata.Code == '50002') {
            throw new Error('Hotel name can not be empty');
        }
        if (json[0].metadata.Code == '50003') {
            throw new Error('Hotel city can not be empty');
        }
        if (json[0].metadata.Code == '50004') {
            throw new Error('You must register to create a hotel');
        }
        throw new Error(json[0].message);
    }
    return json;
};

export const updateRoom = async (hotelId: number, data: HotelData): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (token == null) {
        throw new Error('You must register to create a hotel');
    }
    const response = await fetch(`${API_BASE}Hotels/${hotelId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        const json = await response.json();
        if (json[0].metadata.Code == '50002') {
            throw new Error('Hotel name can not be empty');
        }
        if (json[0].metadata.Code == '50003') {
            throw new Error('Hotel city can not be empty');
        }
        if (json[0].metadata.Code == '50001') {
            throw new Error('You must register to create a hotel');
        }
        throw new Error(json[0].message);
    }
};

export const deleteRoom = async (hotelId: number): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (token == null) {
        throw new Error('You must register to create a hotel');
    }
    const response = await fetch(`${API_BASE}Hotels/${hotelId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${token}`
        },
    });

    if (!response.ok) {
        const json = await response.json();
        if (json[0].metadata.Code == '50001') {
            throw new Error('The hotel cannot be found');
        }

        throw new Error(json[0].message);
    }
};