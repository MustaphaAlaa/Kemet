import { createSlice } from "@reduxjs/toolkit"
import { jwtDecode, type JwtPayload } from "jwt-decode";

interface AuthInitialState {
    user: { username: string, email: string } | null,
    token: string | null,
    roles: string[]
}

interface jwtPayloads extends JwtPayload {
    Roles: string[]
}
const initialState: AuthInitialState = { user: null, token: null, roles: [] }

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setCredentials: (state, action) => {

            const { userName, email, token } = action.payload.result


            if (token) {

                const jw = jwtDecode<jwtPayloads>(token);
                console.log(jw);
                console.log(jw.Roles);
                state.user = { username: userName, email };
                state.roles = jw.Roles;
                state.token = token
            }
            // console.log(state);

        },
        logOut: (state, action) => {
            state.user = null
            state.token = null
        }
    },
})

export const { setCredentials, logOut } = authSlice.actions

export const authReducer = authSlice.reducer

export const selectCurrentUser = (state) => state.auth.user;
export const selectCurrentToken = (state) => state.auth.token;
export const selectUserRoles = (state) => state.auth.roles;