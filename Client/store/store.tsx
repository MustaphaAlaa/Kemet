import { configureStore } from '@reduxjs/toolkit'
import { setupListeners } from "@reduxjs/toolkit/query";
import { persistStore, persistReducer } from 'redux-persist'
import storage from 'redux-persist/lib/storage' // defaults to localStorage for web


import { authReducer } from './slices//authSlice'
import { apiSlice } from './apis/apiSlice'

export { setCredentials, logOut, selectCurrentToken, selectCurrentUser, selectUserRoles } from './slices//authSlice'
export { useLoginMutation } from './apis/authApi';


const persistConfig = { key: 'auth', storage }
const persistedReducer = persistReducer(persistConfig, authReducer);

export const store = configureStore({
    reducer: {
        [apiSlice.reducerPath]: apiSlice.reducer,
        auth: persistedReducer
    },
    middleware: (getDefaultMiddleware) => {
        return getDefaultMiddleware().concat(apiSlice.middleware);
    }
});
setupListeners(store.dispatch);



export type rootState = ReturnType<typeof store.getState>;
export const persister = persistStore(store);