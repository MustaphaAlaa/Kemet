import { useEffect } from "react";
import Button from "../../Components/ReuseableComponents/Button";
import { MdAddCircle } from "react-icons/md";
import { usePortal } from "../../hooks/usePortal";
import useSizeContext from "../../hooks/useSizeContext";
import CreateSize from "./CreateSize";
import SizesList from "./SizesList";
import ApiLinks from "../../APICalls/ApiLinks";

export default function SizeManagement() {
  const { toggle, openPortal, closePortal } = usePortal();

  const {
    getResponseData: getSizes,
    entityAdded: sizeAdded,
    entityUpdated: sizeUpdated,
    entityDeleted: sizeDeleted,
  } = useSizeContext();

  useEffect(() => {
    getSizes(ApiLinks.size.getAll);

    return () => {};
  }, [getSizes, sizeAdded, sizeUpdated, sizeDeleted]);

  const OpenPortal = () => openPortal();
  const handleClose = () => closePortal();

  const crateSize = <CreateSize handleClose={handleClose}></CreateSize>;

  return (
    <div className="justify-between items-center flex flex-col">
      <Button
        className="flex flex-row justify-between gap-3 text-xl"
        roundedMd
        success
        hover
        onClick={OpenPortal}
      >
        إضافة مقاس{" "}
        <span className="text-white shadow rounded-full border border-2 border-green-200">
          <MdAddCircle className="text-xl" />{" "}
        </span>
      </Button>
      {toggle && crateSize}
      <SizesList></SizesList>
    </div>
  );
}
