using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Book))]
public class AutoFlip : MonoBehaviour {
    public FlipMode Mode;
    public float PageFlipTime = 1;
    public float TimeBetweenPages = 0.5f;
    public float DelayBeforeStarting = 0;
    public bool AutoStartFlip=true;
    public Book ControledBook;
    public int AnimationFramesCount = 130;
    bool isFlipping = false;

    public float holdTimeToFlip = 0.1f;  // Tempo in secondi per considerare un "hold"
private float holdTime = 0f;          // Tempo attuale di pressione del tasto
private bool isHoldingRight = false;  // Stato di pressione del tasto destro
private bool isHoldingLeft = false;   // Stato di pressione del tasto sinistro


    // Use this for initialization
    void Start() {
        if (!ControledBook)
            ControledBook = GetComponent<Book>();

        if (AutoStartFlip)
            StartFlipping();

        // Listen for page flip completion
        ControledBook.OnFlip.AddListener(new UnityEngine.Events.UnityAction(PageFlipped));
    }

    void PageFlipped() {
        isFlipping = false;
    }

    // Starts flipping automatically
    public void StartFlipping() {
        StartCoroutine(FlipToEnd());
    }

    // Flip one page to the right
    public void FlipRightPage(bool fromKeyboard = false) {
        if (isFlipping || ControledBook.currentPage >= ControledBook.TotalPageCount) return;
        isFlipping = true;
            isKeyboardFlip = fromKeyboard;  // Attiva l'opzione per il flip da tastiera
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.8f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.8f;
        float dx = (xl) * 3 / AnimationFramesCount;
        StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
    }

    // Flip one page to the left
    public void FlipLeftPage(bool fromKeyboard = false) {
        if (isFlipping || ControledBook.currentPage <= 0) return;
        isFlipping = true;
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.8f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.8f;
        float dx = (xl) * 3 / AnimationFramesCount;
        StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
    }

    // Coroutine for flipping all pages
    IEnumerator FlipToEnd() {
        yield return new WaitForSeconds(DelayBeforeStarting);
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.8f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.8f;
        float dx = (xl) * 3 / AnimationFramesCount;
        switch (Mode) {
            case FlipMode.RightToLeft:
                while (ControledBook.currentPage < ControledBook.TotalPageCount) {
                    StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
            case FlipMode.LeftToRight:
                while (ControledBook.currentPage > 0) {
                    StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
        }
    }

    // Coroutine for Right-to-Left page flip
    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx) {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

        ControledBook.DragRightPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++) {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookRTLToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(frameTime);
            x -= dx;
        }
        ControledBook.ReleasePage();
    }

    // Coroutine for Left-to-Right page flip
    IEnumerator FlipLTR(float xc, float xl, float h, float frameTime, float dx) {
        float x = xc - xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);
        ControledBook.DragLeftPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++) {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookLTRToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(frameTime);
            x += dx;
        }
        ControledBook.ReleasePage();
    }

    // Check keyboard input in Update
    void Update()
{
        if (isFlipping) return;  // Evita di iniziare un nuovo flip mentre è in corso uno attuale

    // Controlla se il tasto destro è tenuto premuto
    if (Input.GetKey(KeyCode.RightArrow))
    {
        if (!isHoldingRight)
        {
            isHoldingRight = true;
            holdTime = 0f; // Resetta il timer
        }
        holdTime += Time.deltaTime; // Aggiungi il tempo di pressione


        // Calcola quante pagine sfogliare
        int pagesToFlip = Mathf.FloorToInt(holdTime / holdTimeToFlip);
        if (pagesToFlip > 0)
        {
            for (int i = 0; i < pagesToFlip; i++)
            {
                FlipRightPage();
            }
            holdTime = 0f; // Resetta il timer dopo il flip
        }
    }
    else
    {
        isHoldingRight = false; // Reset se il tasto non è più premuto
    }

    // Controlla se il tasto sinistro è tenuto premuto
    if (Input.GetKey(KeyCode.LeftArrow))
    {
        if (!isHoldingLeft)
        {
            isHoldingLeft = true;
            holdTime = 0f; // Resetta il timer
        }
        holdTime += Time.deltaTime; // Aggiungi il tempo di pressione

        // Calcola quante pagine sfogliare
        int pagesToFlip = Mathf.FloorToInt(holdTime / holdTimeToFlip);
        if (pagesToFlip > 0)
        {
            for (int i = 0; i < pagesToFlip; i++)
            {
                FlipLeftPage();
            }
            holdTime = 0f; // Resetta il timer dopo il flip
        }
    }
    else
    {
        isHoldingLeft = false; // Reset se il tasto non è più premuto
    }
}

private bool isKeyboardFlip = false;

}