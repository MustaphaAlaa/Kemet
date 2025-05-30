import useNavigationContext from "../../hooks/useNavigationContext"

export default function Route({ path, children }: { path: string, children: any }) {
    const { currentPath } = useNavigationContext();

    console.log(`$ Route: Path ${path}`);
    console.log(`$ Route CurrentPath ${currentPath}`);
    const isPathMatch = path?.toLowerCase() === currentPath?.toLowerCase();
    console.log(`isPathMatch = ${isPathMatch}`)
    if (isPathMatch)
        // return <h1>Here My Route</h1>
    return <div>{children}</div>

    return null;
}