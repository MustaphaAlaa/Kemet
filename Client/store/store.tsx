import { configureStore } from '@reduxjs/toolkit'
import { setupListeners } from "@reduxjs/toolkit/query";
import { authReducer } from './slices//authSlice'
import { apiSlice } from './apis/apiSlice'

export { setCredentials, logOut, selectCurrentToken, selectCurrentUser } from './slices//authSlice'
export { useLoginMutation } from './apis/authApi';

export const store = configureStore({
    reducer: {
        [apiSlice.reducerPath]: apiSlice.reducer,
        auth: authReducer
    },
    middleware: (getDefaultMiddleware) => {
        return getDefaultMiddleware().concat(apiSlice.middleware);
    }
});
setupListeners(store.dispatch);
