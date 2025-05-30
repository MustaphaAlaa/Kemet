
import { useState, type MouseEventHandler } from 'react';
import { MdCheck, MdOutlineQuestionMark } from 'react-icons/md';

export default function OpenClosedLabel({ valueToExpand, expandedValue, click, children }: { valueToExpand: string, click: MouseEventHandler<HTMLDivElement>, expandedValue: string, children: any }) {
    const sharedIconStyle = " text-2xl rounded-lg shadow-sm/20 ";

    const borderRoundedStyle = expandedValue === valueToExpand ? 'rounded-t-lg' : 'rounded-xs';

    // const backgroundGradient = expandedValue !== valueToExpand ? 'from-red-500  via-rose-500  to-rose-400 text-white' : ' from-cyan-700 via-cyan-500 to-cyan-400  ';
    const backgroundGradient = expandedValue !== valueToExpand ? 'from-red-500  via-rose-500  to-rose-400 text-white' :
        'from-blue-400 via-sky-300 to-sky-200 text-sky-900  font-bold ';
    const hover = 'hover:from-emerald-400 hover:via-green-300 hover:to-teal-300 hover:text-lime-800';


    const IconBackground = 'bg-white text-gray-950';
    const [iconHover, setIconHover] = useState(IconBackground)


    const setHoverStyle = () => setIconHover('bg-lime-800 text-white border border-2');
    const setIconBackground = () => setIconHover(IconBackground);


    //  const [Qq, setQq] = useState(<MdOutlineQuestionMark className={`${sharedIconStyle} ${iconHover} shrink`} />);
    const [isMouseEnter, setMouseEnter] = useState(0);

    const handleMouseEnter = () => {
        setHoverStyle();
        setMouseEnter(1);

    };


    const handleMouseLeave = () => {
        setIconBackground();
        setMouseEnter(0);
    };


    const mdCheckSelected = <MdCheck className={`${sharedIconStyle}    border border-2 border-purple-800  bg-yellow-200 text-purple-800 `} />;
    const mdCheck = <MdCheck className={`${sharedIconStyle}    ${iconHover}    `} />;
    const mdOutlineQuestionMark = <MdOutlineQuestionMark className={`${sharedIconStyle} ${iconHover}  `} />

    return <div onMouseEnter={handleMouseEnter} onMouseLeave={handleMouseLeave} className={`${borderRoundedStyle}   flex items-center justify-content bg-gradient-to-r ${hover} ${backgroundGradient}  p-3   text-2xl `} onClick={click}>

        {

            (expandedValue === valueToExpand && isMouseEnter < 1) ? { ...mdCheckSelected } :

                (expandedValue !== valueToExpand && isMouseEnter < 1) ? { ...mdOutlineQuestionMark }

                    : { ...mdCheck }
        }


        {children}
    </div>
}
