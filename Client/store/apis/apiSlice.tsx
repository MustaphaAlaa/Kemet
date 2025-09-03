import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

import { setCredentials } from '../slices/authSlice'
import { logOut } from '../slices/authSlice';
import ApiDomain from '../../src/app/Models/ApiDomain'

const baseQuery = fetchBaseQuery({
    baseUrl: ApiDomain,
    credentials: 'include',
    prepareHeaders: (headers, { getState }) => {
        const token = getState().auth.token;
        if (token) {
            headers.set('authorization', `Bearer ${token}`)
        }
        return headers;
    }
});


const baseQueryWithReauth = async (args, api, extraOptions) => {
    let result = await baseQuery(args, api, extraOptions)

    if (result?.error?.originalStatus === 403) {
        console.log('sending refresh token')
        // send refresh token to get new access token 
        const refreshResult = await baseQuery('/refresh', api, extraOptions)
        console.log(refreshResult)
        if (refreshResult?.data) {
            const user = api.getState().auth.user
            // store the new token 
            api.dispatch(setCredentials({ ...refreshResult.data, user }))
            // retry the original query with new access token 
            result = await baseQuery(args, api, extraOptions)
        } else {
            api.dispatch(logOut({}))
            // Return the refreshResult or the original result to satisfy return type
            return refreshResult
        }
    }
    return result;
}


const apiSlice = createApi({
    baseQuery: baseQueryWithReauth,
    endpoints: builder => ({})
});


export { apiSlice };  
