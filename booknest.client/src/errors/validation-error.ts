import type { UIError } from "../api/response-map";

export class ValidationError extends Error {
    public errors: UIError[];

    constructor(errors: UIError[]) {
        super('Validation failed');
        this.errors = errors;

        Object.setPrototypeOf(this, ValidationError.prototype);
    }
}