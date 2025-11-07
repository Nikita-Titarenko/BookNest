export interface BadRequest {
    errors: ResponseError[]
};

export interface ResponseError {
    field: string,
    code: number,
    message: string
};

export interface UIError {
    field: string,
    message: string
};

export const getResponseErrors = (response: BadRequest): UIError[] => {
    return response.errors.map(e => ({
        field: e.field,
        message: getMessageFromCode(e.code)
    }));
};

export const getMessageFromCode = (code: number): string => {
    switch (code) {
        case 50001:
            return "The hotel owned by this user was not found.";
        case 50002:
            return "Hotel name cannot be empty.";
        case 50003:
            return "Hotel city cannot be empty.";
        case 50004:
            return "User not found.";
        case 50005:
            return "Room name cannot be empty.";
        case 50006:
            return "Room price must be greater than 0.";
        case 50007:
            return "Room quantity must be greater than 0.";
        case 50008:
            return "Number of guests must be greater than 0.";
        case 50009:
            return "Room size must be greater than 0.";
        case 50010:
            return "The hotel owned by this user was not found.";
        case 50011:
            return "The room for this hotel and user was not found.";
        case 50012:
            return "A user with this email and phone already exists.";
        case 50013:
            return "A user with this email already exists.";
        case 50014:
            return "A user with this phone number already exists.";
        case 50015:
            return "All rooms are already booked on this date.";
        case 50016:
            return "This booking already exists.";
        case 50017:
            return "Hotel not found.";
        case 50018:
            return "Room not found.";
        case 50019:
            return "Room ID does not match.";
        case 50020:
            return "Room booking not found.";
        case 50021:
            return "Phone number can contain only + and digits.";
        case 50022:
            return "Email must contain @.";
        case 50023:
            return "Full name must be from 3 to 150 characters.";
        case 50024:
            return "Email must be from 3 to 254 characters.";
        case 50025:
            return "Password must be from 6 to 30 characters.";
        case 50026:
            return "Phone number must be from 9 to 20 digits.";
        case 50027:
            return "Hotel name must be between 3 and 100 characters.";
        case 50028:
            return "Hotel city must be between 3 and 200 characters.";
        case 50029:
            return "Room name must be between 3 and 200 characters.";
        case 50030:
            return "Email or password incorrect.";
        default:
            return "An unexpected error occurred. Please try again.";
    }
};