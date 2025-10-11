import { createSlice } from "@reduxjs/toolkit"
import { jwtDecode, type JwtPayload } from "jwt-decode";

interface AuthInitialState {
    user: { username: string, email: string } | null,
    token: string | null,
    role: string[]
}

interface jwtPayloads extends JwtPayload {
    role: string[]
}
const initialState: AuthInitialState = { user: null, token: null, role: [] }

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setCredentials: (state, action) => {

            const { userName, email, token } = action.payload.result


            if (token) {

                const jw = jwtDecode<jwtPayloads>(token);
                console.log(jw); 
                console.log(jw.role);
                state.user = { username: userName, email };
                state.role = jw.role;
                state.token = token;
                localStorage.setItem('token', token);
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
export const selectUserRoles = (state) => state.auth.role;