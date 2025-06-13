export interface APIResponse<T> {

    statusCode?: number,

    isSuccess?: boolean,

    errorMessages?: string[] | null,

    result?: T | null
}


