export interface APIResponse<T> {

    statusCode: number,

    isSuccess: boolean,

    errorMessages: null | undefined | string[],

    result: T
}