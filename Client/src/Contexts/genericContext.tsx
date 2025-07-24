import React, { createContext, useCallback, useState } from "react"
import type { APIResponse } from "../app/Models/APIResponse";
import axios from "axios";



interface genericCurdContextType<T> {
    response?: APIResponse<T[]>;
    getResponseData: (url: string) => Promise<void>;
    createEntity: (url: string, params: object) => Promise<void>;
    updateEntity: (url: string, params: object) => Promise<void>;
    deleteEntity: (url: string, params: object) => Promise<void>;
    entityAdded: number;
    entityUpdated: number;
    entityDeleted: number;
}

export function createCRUDContext<T>() {
    return createContext<genericCurdContextType<T>>({});
}



export function CreateGenericProvider<T>(context: React.Context<genericCurdContextType<T>>) {
    return function GenericProvider({ children }: { children: React.ReactNode }) {




        const [response, setResponse] = useState<APIResponse<T[]>>();
        const [entityAdded, setEntityIsAdded] = useState(0);
        const [entityUpdated, setEntityIsUpdated] = useState(0);
        const [entityDeleted, setEntityIsDeleted] = useState(0);


        //
        const getResponseData = useCallback(async (url: string) => {
            const { data } = await axios.get(url);
            setResponse(data)
            console.log(data);
        }, []);

        const deleteEntity = async (url: string, params: object) => {
            await axios.delete(url, params)
            setEntityIsDeleted(entityDeleted + 1);
        }

        const createEntity = async (url: string, params: object) => {
            // const { data }: { data: APIResponse<T[]> } = await axios.post(`${domain}api/a/Color/add`, { Name: colorName, HexaCode: hexacode })
            const { data }: { data: APIResponse<T> } = await axios.post(url, params)

            console.log(data);

            if (data.statusCode === 201)
                setEntityIsAdded(entityAdded + 1);


        }





        const updateEntity = async (url: string, params: object) => {
            // const { data }: { data: APIResponse<Color[]> } = await axios.put(`${domain}api/a/Color/`, { ColorId: colorId, Name: colorName, HexaCode: hexacode })
            const { data }: { data: APIResponse<T> } = await axios.put(url, params)

            if (data.statusCode === 200) setEntityIsUpdated(entityUpdated + 1);
        }



        const vals = {
            response,
            getResponseData,
            createEntity,
            entityAdded,
            entityUpdated,
            updateEntity,
            deleteEntity,
            entityDeleted,
        }


        return <context.Provider value={vals}>{children}</context.Provider>

    }
}

