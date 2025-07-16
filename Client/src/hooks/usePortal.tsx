import { useState } from "react";

export function usePortal() {
    const [toggle, setToggle] = useState(false);

    return {
        toggle,
        openPortal(): void { setToggle(true) },
        closePortal(): void { setToggle(false) }
    }
}