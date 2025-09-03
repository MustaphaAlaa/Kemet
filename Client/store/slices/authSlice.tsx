import { createSlice } from "@reduxjs/toolkit"
import { jwtDecode } from "jwt-decode";

interface AuthInitialState {
    user: { username: string, email: string } | null,
    token: string | null,
    roles: string[]
}


const initialState: AuthInitialState = { user: null, token: null, roles: [] }

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setCredentials: (state, action) => {

            const { user, token } = action.payload
            if (token) {
                const { roles } = jwtDecode<AuthInitialState>(token);
                state.user = { username: user.username, email: user.email };
                state.roles = roles;
                state.token = token
            }

        },
        logOut: (state, action) => {
            state.user = null
            state.token = null
        }
    },
})

export const { setCredentials, logOut } = authSlice.actions

export const authReducer = authSlice.reducer

export const selectCurrentUser = (state) => state.auth.user
export const selectCurrentToken = (state) => state.auth.token