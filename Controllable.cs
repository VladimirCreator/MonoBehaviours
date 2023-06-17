using System;
using UnityEngine;

public sealed class Controllable : MonoBehaviour {

    // MARK:- Declarations

    // MARK: Interfaces

    private interface IInputHandler {

        public abstract IInputHandler Delegate { get; set; }

        public abstract void React(Controllable component);
        public abstract void Update(Controllable component);

    }

    // MARK: Classes

    private abstract class InputHandler : IInputHandler {

        public Func<bool> ReactOn;
        public IInputHandler Delegate { get; set; }

        public abstract void React(Controllable component);

        public void Update(Controllable component) {
            if (ReactOn()) {
                React(component);
            }
            Delegate?.Update(component);
        }

    }

    // MARK: ...

    private sealed class UpInputHandler : InputHandler {
        public sealed override void React(Controllable component) => component.transform.position += Vector3.up * Time.deltaTime * component._speedFactor;
    }

    private sealed class DownInputHandler : InputHandler {
        public sealed override void React(Controllable component) => component.transform.position += Vector3.down * Time.deltaTime * component._speedFactor;
    }

    private sealed class LeftInputHandler : InputHandler {
        public sealed override void React(Controllable component) => component.transform.position += Vector3.left * Time.deltaTime * component._speedFactor;
    }

    private sealed class RightInputHandler : InputHandler {
        public sealed override void React(Controllable component) => component.transform.position += Vector3.right * Time.deltaTime * component._speedFactor;
    }

    // MARK:- Properties
    private IInputHandler _inputHandler;

    [SerializeField]
    private bool _controllable = false;

    [SerializeField]
    private float _speedFactor = 1.0f;

    // MARK:- Methods

    internal void Start() {
        InputHandler upInputHandler = new UpInputHandler(),
                     downInputHandler = new DownInputHandler(),
                     leftInputHandler = new LeftInputHandler(),
                     rightInputHandler = new RightInputHandler();
        upInputHandler.Delegate = downInputHandler;
        downInputHandler.Delegate = leftInputHandler;
        leftInputHandler.Delegate = rightInputHandler;

        upInputHandler.ReactOn = () => {
            if (Input.GetKey("up") {
                return true;
            }
            else if (Input.GetKey("w") {
                return true;
            }
            else {
                return false;
            }
        };
        downInputHandler.ReactOn = () => {
            if (Input.GetKey("down") {
                return true;
            }
            else if (Input.GetKey("s") {
                return true;
            }
            else {
                return false;
            }
        };
        leftInputHandler.ReactOn = () => {
            if (Input.GetKey("left") {
                return true;
            }
            else if (Input.GetKey("a") {
                return true;
            }
            else {
                return false;
            }
        };
        rightInputHandler.ReactOn = () => {
            if (Input.GetKey("right") {
                return true;
            }
            else if (Input.GetKey("d") {
                return true;
            }
            else {
                return false;
            }
        };

        _inputHandler = upInputHandler;
    }

    internal void Update() {
        if (_controllable) {
            _inputHandler.Update(this);
        }
    }



}
