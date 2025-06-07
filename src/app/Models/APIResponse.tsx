export interface APIResponse<T> {

    statusCode?: number,

    isSuccess?: boolean,

    errorMessages?: string[],

    result?: T
}


