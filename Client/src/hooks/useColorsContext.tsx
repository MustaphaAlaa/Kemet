import { useContext } from "react"
import { ColorContext } from "../Contexts/color/colorContext";





export default function useColorsContext() {
    return useContext(ColorContext);
}