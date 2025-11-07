import { ValidationError } from "../errors/validation-error";
import { getResponseErrors, type BadRequest } from "./response-map";

const API_BASE = 'https://localhost:7079/api/';

export interface HotelWithRoom {
    hotelId: number,
    hotelName: string,
    hotelCity: string,
    roomId: number,
    roomName: string,
    roomPrice: number
};

export interface HotelData {
    hotelName: string,
    hotelCity: string,
    hotelDescription: string,
};

export interface HotelListItem {
    hotelId: number,
    hotelName: string,
    hotelCity: string,
    totalRoomCount: number
};

export interface CreateHotelResponse {
    hotelId: number,
};

export interface GetHotelNameResponse {
    hotelName: string,
};

export const getHotelsWithCheapestRoom = async (startDate: string, endDate: string, pageNumber: number, pageSize: number, guestsNumber: number | null): Promise<HotelWithRoom[]> => {
    const params = new URLSearchParams({
        startDate: startDate,
        endDate: endDate,
        pageNumber: pageNumber.toString(),
        pageSize: pageSize.toString(),
        ...(guestsNumber !== null && { guestsNumber: guestsNumber.toString() })
    });
    const response = await fetch(`${API_BASE}Hotels/with-cheapest-rooms?${params.toString()}`, {
        method: 'GET'
    });

    const data = await response.json();

    if (!response.ok) {
        throw new ValidationError(getResponseErrors(data as BadRequest));
    }

    return data;
};

export const getHotelsWithMostExpensiveRoom = async (startDate: string, endDate: string, pageNumber: number, pageSize: number, guestsNumber: number | null): Promise<HotelWithRoom[]> => {
    const params = new URLSearchParams({
        startDate: startDate,
        endDate: endDate,
        pageNumber: pageNumber.toString(),
        pageSize: pageSize.toString(),
        ...(guestsNumber !== null && { guestsNumber: guestsNumber.toString() })
    });
    const response = await fetch(`${API_BASE}Hotels/with-most-expensive-rooms?${params.toString()}`, {
        method: 'GET'
    });

    const data = await response.json();

    if (!response.ok) {
        throw new ValidationError(getResponseErrors(data as BadRequest));
    }

    return data;
};

export const getHotel = async (hotelId: number): Promise<HotelData> => {
    const response = await fetch(`${API_BASE}Hotels/${hotelId}`, {
        method: 'GET'
    });

    const data = await response.json();

    if (!response.ok) {
        throw new ValidationError(getResponseErrors(data as BadRequest));
    }

    return data;
};

export const getHotelName = async (hotelId: number): Promise<GetHotelNameResponse> => {
    const response = await fetch(`${API_BASE}Hotels/${hotelId}/name`, {
        method: 'GET'
    });

    const data = await response.json();

    if (!response.ok) {
        throw new ValidationError(getResponseErrors(data as BadRequest));
    }

    return data;
};

export const getHotelsByUser = async (): Promise<HotelListItem[]> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to create a hotel');
    }

    const response = await fetch(`${API_BASE}Hotels/by-user`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        },
    });

    const data = await response.json();

    if (!response.ok) {
        throw new ValidationError(getResponseErrors(data as BadRequest));
    }

    return data;
};

export const createHotel = async (data: HotelData): Promise<CreateHotelResponse> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
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
        throw new ValidationError(getResponseErrors(json as BadRequest));
    }
    return json;
};

export const updateHotel = async (hotelId: number, data: HotelData): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to update a hotel');
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
        throw new ValidationError(getResponseErrors(json as BadRequest));
    }
};

export const deleteHotel = async (hotelId: number): Promise<void> => {
    const token = localStorage.getItem('jwt-token');
    if (!token) {
        throw new Error('You must register to delete a hotel');
    }
    const response = await fetch(`${API_BASE}Hotels/${hotelId}`, {
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