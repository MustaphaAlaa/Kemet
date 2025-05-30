import { createContext, useCallback, useEffect, useState, type JSX } from "react"
 

const navigationContext = createContext({});

export function NavigationProvider({ children }) {

    const [currentPath, setCurrentPath] = useState('/');


    useEffect(() => {
        const handleEvent = () => {
            setCurrentPath(document.location.pathname)
        }

        window.addEventListener('popstate', handleEvent);

        return () => {
            window.removeEventListener('popstate', handleEvent);
        }

    }, [])

    const navigate = (to: string) => {
        window.history.pushState({}, '', `${to}`);
        setCurrentPath(to);
    }
    const vals = {
        currentPath,
        navigate,
    }


    return <navigationContext.Provider value={vals}>
        {children}
    </navigationContext.Provider>

}

export default navigationContext; 