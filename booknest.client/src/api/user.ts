import { getResponseErrors, type BadRequest, type UIError } from "./response-map";
import { ValidationError } from '../errors/validation-error';

const API_BASE = 'https://localhost:7079/api/';

interface RegisterData {
    email: string,
    fullname: string,
    password: string,
    phone: string
};

interface LoginResponse {
    jwtToken: string
};

export const login = async (email: string, password: string): Promise<LoginResponse> => {
    const response = await fetch(`${API_BASE}Users/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, password })
    });

    const json = await response.json();

    if (!response.ok) {
        throw new ValidationError(getResponseErrors(json as BadRequest));
    }

    return json;
};

export const register = async (data: RegisterData): Promise<LoginResponse> => {
    const response = await fetch(`${API_BASE}Users/register`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });
    const json = await response.json();
    if (!response.ok) {
        throw new ValidationError(getResponseErrors(json as BadRequest));
    }

    return json;
};