import { useContext } from "react";
import navigationContext from "../Contexts/navigation";

type navigateContextType = {
    currentPath: string;
    navigate: (to: string) => void;
}

export default function useNavigationContext(): navigateContextType {
    return useContext(navigationContext);
}