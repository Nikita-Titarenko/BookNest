const API_BASE = "https://localhost:7079/api/";

export interface RoomListItem {
    roomId: number,
    roomName: string,
    roomPrice: number,
    roomSize: number,
    roomQuantity: number,
    guestsNumber: number,
};

export interface RoomData {
    roomName: string,
    roomPrice: number,
    roomSize: number,
    roomQuantity: number,
    guestsNumber: number,
};

export interface CreateRoomResponse {
    roomId: number
}

export interface CreateRoomData {
    roomName: string,
    roomPrice: number,
    roomQuantity: number,
    guestsNumber: number,
    roomSize: number,
    hotelId: number
}

export interface UpdateRoomData {
    roomName: string,
    roomPrice: number,
    roomQuantity: number,
    guestsNumber: number,
    roomSize: number,
}

export const getRoom = async (roomId: Number): Promise<RoomData> => {
    const response = await fetch(`${API_BASE}Rooms/${roomId.toString()}`, {
        method: 'GET',
    });

    const data = await response.json();

    if (!response.ok) {
        throw new Error(data[0].metadata.Code);
    }

    return data;
};

export const getRoomsByHotel = async (hotelId: Number, startDate: string | null, endDate: string | null, guestsNumber: number | null): Promise<RoomListItem[]> => {
    const params = new URLSearchParams({
        hotelId: hotelId.toString(),
        ...(startDate != null && { startDate: startDate }),
        ...(endDate != null && { endDate: endDate }),
        ...(guestsNumber != null && { guestsNumber: guestsNumber.toString() })
    });

    const response = await fetch(`${API_BASE}Rooms/by-hotel?${params.toString()}`, {
        method: 'GET'
    });

    const data = await response.json();

    if (!response.ok) {
        throw new Error(data[0].metadata.Code);
    }

    return data;
};

export const createRoom = async (data: CreateRoomData): Promise<CreateRoomResponse> => {
    const token = localStorage.getItem('jwt-token');
    if (token == null) {
        throw new Error('You must register to create a hotel');
    }
    const response = await fetch(`${API_BASE}Rooms`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
    });
    const json = await response.json();

    if (!response.ok) {
        if (json[0].metadata.Code == '50005') {
            throw new Error(`Field 'Room name' cannot be empty`);
        }
        if (json[0].metadata.Code == '50006') {
            throw new Error(`Field 'Room price' cannot be empty, equal or less than 0`);
        }
        if (json[0].metadata.Code == '50007') {
            throw new Error(`Field 'Room quantity' cannot be empty, equal or less than 0`);
        }
        if (json[0].metadata.Code == '50008') {
            throw new Error(`Field 'Guests number' cannot be empty, equal or less than 0`);
        }
        if (json[0].metadata.Code == '50009') {
            throw new Error(`Field 'Room size' cannot be empty, equal or less than 0`);
        }
        throw new Error(json[0].message);
    }
    return json;
};

export const updateRoom = async (roomId: number, data: UpdateRoomData): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (token == null) {
        throw new Error('You must register to create a hotel');
    }
    const response = await fetch(`${API_BASE}Rooms/${roomId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
    });

    if (!response.ok) {
        const json = await response.json();
        if (json[0].metadata.Code == '50005') {
            throw new Error(`Field 'Room name' cannot be empty`);
        }
        if (json[0].metadata.Code == '50006') {
            throw new Error(`Field 'Room price' cannot be empty, equal or less than 0`);
        }
        if (json[0].metadata.Code == '50007') {
            throw new Error(`Field 'Room quantity' cannot be empty, equal or less than 0`);
        }
        if (json[0].metadata.Code == '50008') {
            throw new Error(`Field 'Guests number' cannot be empty, equal or less than 0`);
        }
        if (json[0].metadata.Code == '50009') {
            throw new Error(`Field 'Room size' cannot be empty, equal or less than 0`);
        }
        if (json[0].metadata.Code == '50011') {
            throw new Error(`A room that belongs to this hotel and owned by this user cannot be found`);
        }
        throw new Error(json[0].message);
    }
};

export const deleteRoom = async (roomId: number): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (token == null) {
        throw new Error('You must register to create a hotel');
    }
    const response = await fetch(`${API_BASE}Rooms/${roomId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${token}`
        },
    });

    if (!response.ok) {
        const json = await response.json();
        if (json[0].metadata.Code == '50011') {
            throw new Error('A room that belongs to this hotel and owned by this user cannot be found');
        }

        throw new Error(json[0].message);
    }
};