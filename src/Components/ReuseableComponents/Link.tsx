import useNavigationContext from "../../hooks/useNavigationContext";
import classNames from "classnames";


export default function Link({ to, children, className = '', activeClassName = '' }) {
  const { navigate }: { navigate: (to: string) => void } = useNavigationContext();

  // const classes = classNames("text-blue-500", className, activeClassName);
  const classes = classNames("", className, activeClassName);



  const handleClick = (event) => {
    console.log(event);
    // if (event.metaKey || event.ctrlKey) return;
    if (event.metaKey || event.ctrlKey) return;

    event.preventDefault();
    // console.log(to);
    navigate(to);
  };

  return (

    <a href={to} onClick={handleClick} className={classes}>
      {children}
    </a>
  );
}
