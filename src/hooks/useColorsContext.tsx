import { useContext, type JSX } from "react"
import colorsContext from "../Contexts/colors";
import type { APIResponse } from "../app/Models/APIResponse";
import type { Color } from "../app/Models/Color";




type colorContextType = {
    colors: APIResponse<Color[]> | undefined;
    getColors: () => Promise<void>;
    colorAdded: boolean;
    colorUpdated: boolean;
    deleteColor: (colorId: number) => Promise<void>;
    colorDeleted: boolean;
    createColor: ({ colorName, hexacode }: {
        colorName: string;
        hexacode: string;
    }) => Promise<void>;
    updateColor: ({ colorId, colorName, hexacode }: {
        colorId: number;
        colorName: string;
        hexacode: string;
    }) => Promise<void>;
    updateState: boolean;

    openUpdateForm: ({ color, setUpdateMode }: {
        color: Color;
        setUpdateMode: any;
    }) => JSX.Element | undefined

}



export default function useColorsContext(): colorContextType {
    return useContext(colorsContext);
}