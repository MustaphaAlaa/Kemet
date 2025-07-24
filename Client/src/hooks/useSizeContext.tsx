import { useContext } from "react" 
import { SizeContext } from "../Contexts/size/sizeContext";


export default function useSizeContext()  {
    return useContext(SizeContext);
}