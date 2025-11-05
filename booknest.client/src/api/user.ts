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
        if (json[0].metadata.Code === 50022) {
            throw new Error('Email must contain @');
        }
        if (json[0].metadata.Code === 50030) {
            throw new Error('Email or password incorrect');
        }
        throw new Error(json[0].message);
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
        if (json[0].metadata.Code === 50021) {
            throw new Error('Phone number must contain only + and digits');
        }
        if (json[0].metadata.Code === 50022) {
            throw new Error('Email must contain @');
        }
        if (json[0].metadata.Code === 50012) {
            throw new Error('User with this email and phone already exists');
        }
        if (json[0].metadata.Code === 50013) {
            throw new Error('User with this email already exists');
        }
        if (json[0].metadata.Code === 50014) {
            throw new Error('User with this phone number already exists');
        }
        throw new Error(json[0].message);
    }
    return json;
};