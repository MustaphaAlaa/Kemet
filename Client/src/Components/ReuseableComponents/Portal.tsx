
import { useEffect } from "react";
import { createPortal } from "react-dom";








type portalArgument = { style: string, handleClose: () => void, actionBar: JSX.Element, [x: string]: any }
export default function Portal({ handleClose, children, actionBar, style = 'bg-gray-100' }: portalArgument) {

    useEffect(() => {
        document.body.classList.add('overflow-hidden');
        return () => {
            document.body.classList.remove('overflow-hidden');
        }
    }, [])



    return createPortal(
        <div>
            <div onClick={handleClose} className="fixed inset-0 opacity-80 bg-gray-800" ></div>
            <div className={`fixed inset-40 ${style}`}>
                <div className="flex flex-col">
                    <div>
                        {children}
                    </div>
                    <div>
                        {actionBar}
                    </div>
                </div>
            </div>
        </div>

        ,
        document.querySelector('.portal-container')!
    );
}
