import { Action, Reducer } from 'redux';
import { userService } from '../services/userService';
import { AppThunkAction } from './';
import {history} from '../helpers/history';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface UsersState {
    isLoading: boolean;
    users: Users;
    userStorage:UserStorage;
}

export interface Users {
    orgId:string;     
    guid: string;
    code: string;
    name: string;
    email: string;
    profileImage: string;
    passwordHash: string;
    groups: string;
    apiKey: string;
    disabled:Number;
    mobile: string;
    orgunitId: string;
    updatedat:string;
    createdat:string;
}
export interface UserStorage {
    orgId:string;
    name: string;
    mobile:string;
    email: string;
    code: string;
    password: string;
    token: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestUsersAction {
    type: 'REQUEST_WEATHER_USERS';
}

interface ReceiveUsersAction {
    type: 'RECEIVE_WEATHER_USERS';
    users: Users;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestUsersAction | ReceiveUsersAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestUsers: (code: string,password: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.Users && code !== appState.Users.users.code) {
            // fetch('users/authenticate', {
            //     method: 'POST',
            //     headers: {
            //         'Content-Type': 'application/json'
            //     },
            //     body: JSON.stringify({
            //         code: code,
            //         password:password
            //     })
            // }).then(response => {
            //     console.log(response)
            // })

            userService.login(code, password)
            .then(
                user => {
                    dispatch({ type: 'RECEIVE_WEATHER_USERS',  users: user });
                    history.push('/');
                },
                error => {
                    dispatch({ type: 'REQUEST_WEATHER_USERS'});
                }
            );
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: UsersState = { users:{    orgId:"1",
    guid: "",
    code: "",
    name: "",
    email: "",
    profileImage: "",
    passwordHash: "",
    groups: "",
    apiKey: "",
    disabled:0,
    mobile: "",
    orgunitId: "",
    updatedat:"",
    createdat:""
}, isLoading: false,userStorage:{
    orgId:"1",
    name: "",
    mobile:"",
    email: "",
    code: "",
    password: "",
    token: "",
} };

export const reducer: Reducer<UsersState> = (state: UsersState | undefined, incomingAction: Action): UsersState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_WEATHER_USERS':
            return {
                users: state.users,
                isLoading: true,
                userStorage:state.userStorage
            };
        case 'RECEIVE_WEATHER_USERS':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            if (action.users.code === state.users.code) {
                return {
                    users: action.users,
                    isLoading: false,
                    userStorage:state.userStorage
                };
            }
            break;
    }

    return state;
};
