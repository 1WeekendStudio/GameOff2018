using UnityEngine;

public interface ICursor
{
    void OnActivate(object parameter);

    void Update(Camera camera);

    void OnDeactivate();

    void OnDrawGizmos();
}
