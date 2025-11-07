import { ValidationError } from "../errors/validation-error";
import { getResponseErrors, type BadRequest } from "./response-map";

const API_BASE = 'https://localhost:7079/api/';

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
        throw new ValidationError(getResponseErrors(data as BadRequest));
    }

    return data;
};

export const getRoomsByHotel = async (hotelId: Number, startDate: string | null, endDate: string | null, guestsNumber: number | null): Promise<RoomListItem[]> => {
    const params = new URLSearchParams({
        hotelId: hotelId.toString(),
        ...(startDate !== null && { startDate: startDate }),
        ...(endDate !== null && { endDate: endDate }),
        ...(guestsNumber !== null && { guestsNumber: guestsNumber.toString() })
    });

    const response = await fetch(`${API_BASE}Rooms/by-hotel?${params.toString()}`, {
        method: 'GET'
    });

    const data = await response.json();

    if (!response.ok) {
        throw new ValidationError(getResponseErrors(data as BadRequest));
    }

    return data;
};

export const createRoom = async (data: CreateRoomData): Promise<CreateRoomResponse> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
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
        throw new ValidationError(getResponseErrors(json as BadRequest));
    }
    return json;
};

export const updateRoom = async (roomId: number, data: UpdateRoomData): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
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
        throw new ValidationError(getResponseErrors(json as BadRequest));
    }
};

export const deleteRoom = async (roomId: number): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
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
        throw new ValidationError(getResponseErrors(json as BadRequest));
    }
};